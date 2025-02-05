package com.twilio.oai;

import java.util.*;

import lombok.RequiredArgsConstructor;

public class Inflector {

    ExpressionRule ruleBook = new ExpressionRule();

    public  String singular(String word) {
        if (word == null) return null;
        String wordStr = word.trim();
        if (wordStr.length() == 0) return wordStr;
        if (ruleBook.isIrregular(wordStr) || isAbbreviation(wordStr)) return wordStr;
        if (ruleBook.isWildCard(wordStr)) {
            return ruleBook.getWildCardMapping(wordStr);
        }
        return ruleBook.getReplacementString(word);
    }

    public boolean isAbbreviation(String word) {
        return word.chars().allMatch(Character::isUpperCase);
    }

     static class ExpressionRule {
        private final List<PluralRule> pluralRules = new ArrayList<>();
        private final List<String> irregulars = new ArrayList<>();
        private final Map<String, String> wildCardMapping = new HashMap<>();

         @RequiredArgsConstructor
         private static class PluralRule {
             private final String expression;
             private final String replacement;
         }

         public ExpressionRule() {
             this.initIrregulars();
             this.initRules();
             this.initWildCardMapping();
         }

         public void addRule(String expression, String replacement) {
             pluralRules.add(new PluralRule(expression, replacement));
         }

         public void initRules() {
            this.addRule("(s|si|u)s$", "$1s"); // '-us' and '-ss' are already singular
            this.addRule("(n)ews$", "$1ews");
            this.addRule("([ti])a$", "$1um");
            this.addRule("((a)naly|(b)a|(d)iagno|(p)arenthe|(p)rogno|(s)ynop|(t)he)ses$", "$1$2sis");
            this.addRule("(^analy)ses$", "$1sis");
            this.addRule("(^analy)sis$", "$1sis"); // already singular, but ends in 's'
            this.addRule("([^f])ves$", "$1fe");
            this.addRule("(hive)s$", "$1");
            this.addRule("(tive)s$", "$1");
            this.addRule("([lr])ves$", "$1f");
            this.addRule("([^aeiouy]|qu)ies$", "$1y");
            this.addRule("(s)eries$", "$1eries");
            this.addRule("(m)ovies$", "$1ovie");
            this.addRule("(x|ch|ss|sh)es$", "$1");
            this.addRule("([m|l])ice$", "$1ouse");
            this.addRule("(bus)es$", "$1");
            this.addRule("(o)es$", "$1");
            this.addRule("(shoe)s$", "$1");
            this.addRule("(cris|ax|test)is$", "$1is"); // already singular, but ends in 's'
            this.addRule("(cris|ax|test)es$", "$1is");
            this.addRule("(octop|vir)i$", "$1us");
            this.addRule("(octop|vir)us$", "$1us"); // already singular, but ends in 's'
            this.addRule("(alias|status)es$", "$1");
            this.addRule("(alias|status)$", "$1"); // already singular, but ends in 's'
            this.addRule("^(ox)en", "$1");
            this.addRule("(vert|ind)ices$", "$1ex");
            this.addRule("(matr)ices$", "$1ix");
            this.addRule("(quiz)zes$", "$1");
            this.addRule("s$", "");
        }

         private void initIrregulars() {
             irregulars.add("AuthTypes");
             irregulars.add("AuthTypeCalls");
             irregulars.add("AuthTypeRegistrations");
             irregulars.add("Aws");
             irregulars.add("BuildStatus");
             irregulars.add("DialingPermissions");
             irregulars.add("Media");
         }

         public boolean isIrregular(String word) {
            return this.irregulars.contains(word);
        }

         public String getReplacementString(String word) {
             for (final PluralRule rule : this.pluralRules) {
                 final String replaced = word.replaceAll("(?i)" + rule.expression, rule.replacement);
                 if (!replaced.equals(word)) {
                     return replaced;
                 }
             }
             return word;
         }

         public boolean isWildCard(String wordStr) {
             return wildCardMapping.containsKey(wordStr);
         }

         public String getWildCardMapping(String wordStr) {
             return wildCardMapping.get(wordStr);
         }

         public void initWildCardMapping() {
             wildCardMapping.put("ESimProfiles", "EsimProfile");
         }
     }
}
