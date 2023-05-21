export class TechniqueType {
    public url: string;
    public name: string;
    public search: string;

    public constructor(_url: string, _name: string, _search: string) {
        this.url = _url;
        this.name = _name;
        this.search = _search;
    }
}