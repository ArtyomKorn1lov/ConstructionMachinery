export class TechniqueType {
    first_url: string;
    first_name: string;
    second_url: string;
    second_name: string;

    public constructor(_first_url: string, _first_name: string, _second_url: string, _second_name: string) {
        this.first_url = _first_url;
        this.first_name = _first_name;
        this.second_url = _second_url;
        this.second_name = _second_name;
    }
}