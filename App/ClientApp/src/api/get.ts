import {apiUrl} from "../default";

export declare type JsonApiResource = {
    id: string,
    links: { self: string },
    type: string,
    attributes: {
        [index: string]: any
    }
}
export declare type ApiGetAllResponse = {
    data: JsonApiResource[],
    links: { [index: string]: string },
    meta: {
        total: number
    }
}
export declare type ApiGetRequest = {
    include?: string[]
}
export declare type ApiGetAllRequest = ApiGetRequest & {
    pagination: {
        number: number,
        size: number
    },
    sort?: Array<{resource?: string, fields: string[]}>,
    like?: string,
    fields?: Array<{key: string, val: string[]}>
    filter?: {
        startsWith?: { key: string, val: string },
        endsWith?: { key: string, val: string },
        lessThan?: { key: string, val: string },
        greaterThan?: { key: string, val: string },
        greaterOrEqual?: { key: string, val: string },
        lessOrEqual?: { key: string, val: string },
        equals: { key: string, val: string }
        not?: { key: string, val: string },
        has?: { key: string },
        any?: { key: string, val: string[] },
        or?: { key: string, val: string[] },
        and?: { key: string, val: string[] },
    }
}
const strAddQuotes = (str: string):string => {
    return "'" + str + "'";
}
const strAddBrackets = (str: string):string => {
    return "(" + str + ")";
}
const strAddSquareBrackets = (str: string):string => {
    return "[" + str + "]";
}
export const getToUrl = (endPoint: string, apiRequest?: ApiGetRequest): string => {
    let url = new URL(apiUrl + endPoint),
        query: Array<[string, string]> = []
    ;

    if (apiRequest?.include) {
        apiRequest.include.forEach(attr => {
            query.push(['include', attr])
        })
    }

    query.forEach(qItem => {
        const key = qItem[0];
        const val = qItem[1];
        url.searchParams.append(key, val)
    })
    
    return url.toString();
}
export const getAllToUrl = (endPoint: string, apiRequest: ApiGetAllRequest) : string => {
    let url = new URL(apiUrl + endPoint),
        query: Array<[string, string]> = []
    ;

    query.push(['page[size]', apiRequest.pagination.size.toString()]);
    query.push(['page[number]', apiRequest.pagination.number.toString()]);

    if (apiRequest?.like) {
        // ?like=cats
        query.push(['like', apiRequest.like]);
    }
    
    if (apiRequest?.include) {
        apiRequest.include.forEach(attr => {
            query.push(['include', attr])
        })
    }
    
    if (apiRequest?.sort) {
        apiRequest.sort.forEach(el => {
            // ?sort=title,createdAt...
            let sortName = 'sort';
            if (el?.resource) {
                // ?sort[articles]=title,createdAt...
                sortName += strAddSquareBrackets(el.resource);    
            }

            query.push([sortName, el.fields.join(',')]);
        })
    }
    
    if (apiRequest?.fields) {
        apiRequest.fields.forEach(el => {
            query.push(['fields' + strAddSquareBrackets(el.key), el.val.join(',')]);
        })
    }

    if (apiRequest.filter) {
        for (const [operation, itemValue] of Object.entries(apiRequest.filter)) {
            if (operation === 'any') {
                let value = itemValue as any as { key: string, val: string[] };
                let queryStr = '';
                value.val.forEach(word => {
                    queryStr += ',' + strAddQuotes(word);
                });
                // ?filter=any(chapter,'Intro','Summary','Conclusion')
                query.push(['filter', operation + strAddBrackets(value.key + queryStr)])
            } else if (['or', 'and'].includes(operation)) {
                let value = itemValue as any as string[];
                // ?filter=or(has(orders),has(invoices))
                query.push(['filter', operation + strAddBrackets(value.join(','))])
            } else if (operation === 'has'){
                let value = itemValue as any as string;
                // ?filter=has(articles)
                query.push(['filter', operation + strAddBrackets(value)])
            } else {
                let value = itemValue as any as { key: string, val: string };
                // ?filter=equals(lastName,'Smith')
                query.push(['filter', operation + strAddBrackets(value.key + ',' + strAddQuotes(value.val))])
            }
        }
    }

    query.forEach(qItem => {
        const key = qItem[0];
        const val = qItem[1];
        url.searchParams.append(key, val)
    })

    return url.toString();
}