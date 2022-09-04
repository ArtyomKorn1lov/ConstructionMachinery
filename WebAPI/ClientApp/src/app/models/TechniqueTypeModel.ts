export class TechniqueType {
    first_url: string;
    first_name: string;
    first_search: string;
    second_url: string;
    second_name: string;
    second_search: string;

    public constructor(_first_url: string, _first_name: string, _first_search: string, _second_url: string, _second_name: string, _second_search: string) {
        this.first_url = _first_url;
        this.first_name = _first_name;
        this.first_search = _first_search;
        this.second_url = _second_url;
        this.second_name = _second_name;
        this.second_search = _second_search;
    }
}