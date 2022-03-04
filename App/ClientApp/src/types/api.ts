import {Model, ToManyRelation, ToOneRelation, PaginationStrategy} from 'coloquent';

abstract class ApiBase extends Model
{
    static jsonApiBaseUrl = 'https://localhost:7125/api';
    static pageSize = 50;
    static paginationStrategy = PaginationStrategy.PageBased;
    // static paginationPageNumberParamName: string = 'page';
    // static paginationPageSizeParamName: string = 'limit';

    public getAttr(attributeName: string): any {
        return this.getAttribute(attributeName);
    };
    public setAttr(attributeName: string, value: any) {
        this.setAttribute(attributeName, value);
    }
}

export class ApiPerson extends ApiBase {
    static jsonApiType = 'persons';
    // getName(): string
    // {
    //     return this.getAttribute('name');
    // }
    //
    // getRating(): number
    // {
    //     return this.getAttribute('rating');
    // }
}


// export class ApiPerson extends Model
// {
//     static jsonApiBaseUrl = 'https://localhost:7125/api'
//     static jsonApiType = 'persons'
//     static pageSize = 30
// }