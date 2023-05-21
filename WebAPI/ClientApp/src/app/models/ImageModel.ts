export class ImageModel {
    public id: number;
    public path: string;
    public relativePath: string;
    public advertId: number;

    public constructor(_id: number, _path: string, _relativePath: string, _advertId: number) {
        this.id = _id;
        this.path = _path;
        this.relativePath = _relativePath;
        this.advertId = _advertId;
    }
}