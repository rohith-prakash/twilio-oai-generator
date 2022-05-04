/**
 * Twilio - Accounts
 * This is the public Twilio REST API.
 *
 * The version of the OpenAPI document: 1.11.0
 * Contact: support@twilio.com
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import { inspect } from 'util';


/**
 * Options to pass to create a AWInstance
 *
 * @property { string } testString 
 * @property { boolean } [testBoolean] 
 * @property { number } [testInteger] 
 * @property { number } [testNumber] 
 * @property { number } [testNumberFloat] 
 * @property { number } [testNumberDouble] 
 * @property { number } [testNumberInt32] 
 * @property { number } [testNumberInt64] 
 * @property { object } [testObject] 
 * @property { Date } [testDateTime] 
 * @property { string } [testDate] 
 * @property { string } [testEnum] 
 * @property { Array<object> } [testObjectArray] 
 * @property { any } [testAnyType] 
 */
export interface AWInstanceCreateOptions {
    testString: string;
    testBoolean?: boolean;
    testInteger?: number;
    testNumber?: number;
    testNumberFloat?: number;
    testNumberDouble?: number;
    testNumberInt32?: number;
    testNumberInt64?: number;
    testObject?: object;
    testDateTime?: Date;
    testDate?: string;
    testEnum?: string;
    testObjectArray?: Array<object>;
    testAnyType?: any;
}
/**
 * Options to pass to page a AWInstance
 *
 * @property { number } [pageSize] 
 */
export interface AWInstancePageOptions {
    pageSize?: number;
}

/**
 * Options to pass to update a AWInstance
 *
 * @property { string } [testString] 
 */
export interface AWInstanceUpdateOptions {
    testString?: string;
}

export class AWListInstance {
    protected _solution: any;
    protected _uri: string;


    constructor(protected _version: Version) {
        this._solution = {  };
        this._uri = `/v1/Credentials/AWS`;
    }

    /**
     * Create a AWInstance
     *
     * @param { AWInstanceCreateOptions } params - Parameter for request
     * @param { function } [callback] - Callback to handle processed record
     *
     * @returns { Promise } Resolves to processed AWInstance
     */
    public async create(params: AWInstanceCreateOptions, callback?: (error: Error | null, item?: AWInstance) => any): Promise<AWInstance>;
    public async create(params: any, callback?: any): Promise<AWInstance> {

        if (params === null || params === undefined) {
            throw new Error('Required parameter "params" missing.');
        }

        if (params.testString === null || params.testString === undefined) {
            throw new Error('Required parameter "params.testString" missing.');
        }

        const data: any = {};

        data['TestString'] = params.testString;
        if (params.testBoolean !== undefined) data['TestBoolean'] = params.testBoolean;
        if (params.testInteger !== undefined) data['TestInteger'] = params.testInteger;
        if (params.testNumber !== undefined) data['TestNumber'] = params.testNumber;
        if (params.testNumberFloat !== undefined) data['TestNumberFloat'] = params.testNumberFloat;
        if (params.testNumberDouble !== undefined) data['TestNumberDouble'] = params.testNumberDouble;
        if (params.testNumberInt32 !== undefined) data['TestNumberInt32'] = params.testNumberInt32;
        if (params.testNumberInt64 !== undefined) data['TestNumberInt64'] = params.testNumberInt64;
        if (params.testObject !== undefined) data['TestObject'] = params.testObject;
        if (params.testDateTime !== undefined) data['TestDateTime'] = params.testDateTime;
        if (params.testDate !== undefined) data['TestDate'] = params.testDate;
        if (params.testEnum !== undefined) data['TestEnum'] = params.testEnum;
        if (params.testObjectArray !== undefined) data['TestObjectArray'] = params.testObjectArray;
        if (params.testAnyType !== undefined) data['TestAnyType'] = params.testAnyType;

        const headers: any = {
            'Content-Type': 'application/x-www-form-urlencoded'
        };


        let promise = this._version.create({ uri: this._uri, method: 'POST', data, headers });

        promise = promise.then(payload => new AWInstance(this._version, payload));

        if (typeof callback === 'function') {
            promise = promise
                .then(value => callback(null, value))
                .catch(error => callback(error));
        }

        return promise;
    }

    /**
     * Page a AWInstance
     *
     * @param { function } [callback] - Callback to handle processed record
     *
     * @returns { Promise } Resolves to processed AWInstance
     */
    public async page(callback?: (error: Error | null, item?: AWInstance) => any): Promise<AWInstance>;
    /**
     * Page a AWInstance
     *
     * @param { AWInstancePageOptions } params - Parameter for request
     * @param { function } [callback] - Callback to handle processed record
     *
     * @returns { Promise } Resolves to processed AWInstance
     */
    public async page(params: AWInstancePageOptions, callback?: (error: Error | null, item?: AWInstance) => any): Promise<AWInstance>;
    public async page(params?: any, callback?: any): Promise<AWInstance> {

        if (typeof params === 'function') {
            callback = params;
            params = {};
        } else {
            params = params || {};
        }

        const data: any = {};


        const headers: any = {
            'Content-Type': 
        };


        let promise = this._version.page({ uri: this._uri, method: 'GET', data, headers });

        promise = promise.then(payload => new AWInstance(this._version, payload));

        if (typeof callback === 'function') {
            promise = promise
                .then(value => callback(null, value))
                .catch(error => callback(error));
        }

        return promise;
    }

    /**
     * Provide a user-friendly representation
     *
     * @returns Object
     */
    toJSON() {
        return this._solution;
    }

    [inspect.custom](depth, options) {
        return inspect(this.toJSON(), options);
    }
}


export class AWContext {
    protected _solution: any;
    protected _uri: string;


    constructor(protected _version: Version, sid: string) {
        this._solution = { sid };
        this._uri = `/v1/Credentials/AWS/${sid}`;
    }

    /**
     * Remove a AWInstance
     *
     * @param { function } [callback] - Callback to handle processed record
     *
     * @returns { Promise } Resolves to processed AWInstance
     */
    public async remove(callback?: (error: Error | null, item?: AWInstance) => any): Promise<AWInstance> { 


        let promise = this._version.remove({ uri: this._uri, method: 'DELETE' });

        promise = promise.then(payload => new AWInstance(this._version, payload, this._solution.sid));

        if (typeof callback === 'function') {
            promise = promise
                .then(value => callback(null, value))
                .catch(error => callback(error));
        }

        return promise;
    }

    /**
     * Fetch a AWInstance
     *
     * @param { function } [callback] - Callback to handle processed record
     *
     * @returns { Promise } Resolves to processed AWInstance
     */
    public async fetch(callback?: (error: Error | null, item?: AWInstance) => any): Promise<AWInstance> { 


        let promise = this._version.fetch({ uri: this._uri, method: 'GET' });

        promise = promise.then(payload => new AWInstance(this._version, payload, this._solution.sid));

        if (typeof callback === 'function') {
            promise = promise
                .then(value => callback(null, value))
                .catch(error => callback(error));
        }

        return promise;
    }

    /**
     * Update a AWInstance
     *
     * @param { function } [callback] - Callback to handle processed record
     *
     * @returns { Promise } Resolves to processed AWInstance
     */
    public async update(callback?: (error: Error | null, item?: AWInstance) => any): Promise<AWInstance>;
    /**
     * Update a AWInstance
     *
     * @param { AWInstanceUpdateOptions } params - Parameter for request
     * @param { function } [callback] - Callback to handle processed record
     *
     * @returns { Promise } Resolves to processed AWInstance
     */
    public async update(params: AWInstanceUpdateOptions, callback?: (error: Error | null, item?: AWInstance) => any): Promise<AWInstance>;
    public async update(params?: any, callback?: any): Promise<AWInstance> {

        if (typeof params === 'function') {
            callback = params;
            params = {};
        } else {
            params = params || {};
        }

        const data: any = {};

        if (params.testString !== undefined) data['TestString'] = params.testString;

        const headers: any = {
            'Content-Type': 'application/x-www-form-urlencoded'
        };


        let promise = this._version.update({ uri: this._uri, method: 'POST', data, headers });

        promise = promise.then(payload => new AWInstance(this._version, payload, this._solution.sid));

        if (typeof callback === 'function') {
            promise = promise
                .then(value => callback(null, value))
                .catch(error => callback(error));
        }

        return promise;
    }

    /**
     * Provide a user-friendly representation
     *
     * @returns Object
     */
    toJSON() {
        return this._solution;
    }

    [inspect.custom](depth, options) {
        return inspect(this.toJSON(), options);
    }
}
export type TestEnumEnumType = 'DialVerb'|'Trunking';

export class AWInstance {
    protected _solution: any;
    protected _context?: AWContext;

    constructor(protected _version: Version, payload, sid?: string) {
        this.accountSid = payload.account_sid;
        this.sid = payload.sid;
        this.testString = payload.test_string;
        this.testInteger = payload.test_integer;
        this.testObject = payload.test_object;
        this.testDateTime = payload.test_date_time;
        this.testNumber = payload.test_number;
        this.priceUnit = payload.price_unit;
        this.testNumberFloat = payload.test_number_float;
        this.testEnum = payload.test_enum;
        this.testArrayOfIntegers = payload.test_array_of_integers;
        this.testArrayOfArrayOfIntegers = payload.test_array_of_array_of_integers;
        this.testArrayOfObjects = payload.test_array_of_objects;

        this._solution = { sid: sid || this.sid };
    }

    private get _proxy(): AWContext {
        this._context = this._context || new AWContext(this._version, this._solution.sid);
        return this._context;
    }

    accountSid?: string | null;
    sid?: string | null;
    testString?: string | null;
    testInteger?: number | null;
    testObject?: TestResponseObjectTestObject | null;
    testDateTime?: string | null;
    testNumber?: number | null;
    priceUnit?: string | null;
    testNumberFloat?: number | null;
    testEnum?: TestResponseObject.TestEnumEnum;
    testArrayOfIntegers?: Array<number>;
    testArrayOfArrayOfIntegers?: Array<Array<number>>;
    testArrayOfObjects?: Array<TestResponseObjectTestArrayOfObjects> | null;

    /**
     * Remove a AWInstance
     *
     * @param { function } [callback] - Callback to handle processed record
     *
     * @returns { Promise } Resolves to processed AWInstance
     */
    public async remove(callback?: (error: Error | null, item?: AWInstance) => any): Promise<AWInstance> { 

        return this._proxy.remove(callback);
    }

    /**
     * Fetch a AWInstance
     *
     * @param { function } [callback] - Callback to handle processed record
     *
     * @returns { Promise } Resolves to processed AWInstance
     */
    public async fetch(callback?: (error: Error | null, item?: AWInstance) => any): Promise<AWInstance> { 

        return this._proxy.fetch(callback);
    }

    /**
     * Update a AWInstance
     *
     * @param { function } [callback] - Callback to handle processed record
     *
     * @returns { Promise } Resolves to processed AWInstance
     */
    public async update(callback?: (error: Error | null, item?: AWInstance) => any): Promise<AWInstance>;
    /**
     * Update a AWInstance
     *
     * @param { AWInstanceUpdateOptions } params - Parameter for request
     * @param { function } [callback] - Callback to handle processed record
     *
     * @returns { Promise } Resolves to processed AWInstance
     */
    public async update(params: AWInstanceUpdateOptions, callback?: (error: Error | null, item?: AWInstance) => any): Promise<AWInstance>;
    public async update(params?: any, callback?: any): Promise<AWInstance> {

        return this._proxy.update(params, callback);
    }

    /**
     * Provide a user-friendly representation
     *
     * @returns Object
     */
    toJSON() {
        return {
            accountSid: this.accountSid, 
            sid: this.sid, 
            testString: this.testString, 
            testInteger: this.testInteger, 
            testObject: this.testObject, 
            testDateTime: this.testDateTime, 
            testNumber: this.testNumber, 
            priceUnit: this.priceUnit, 
            testNumberFloat: this.testNumberFloat, 
            testEnum: this.testEnum, 
            testArrayOfIntegers: this.testArrayOfIntegers, 
            testArrayOfArrayOfIntegers: this.testArrayOfArrayOfIntegers, 
            testArrayOfObjects: this.testArrayOfObjects
        }
    }

    [inspect.custom](depth, options) {
        return inspect(this.toJSON(), options);
    }
}
