export class ImageModel {
    id: number;
    path: string;
    relativePath: string;
    advertId: number;

    public constructor(_id: number, _path: string, _relativePath: string, _advertId: number) {
        this.id = _id;
        this.path = _path;
        this.relativePath = _relativePath;
        this.advertId = _advertId;
    }
}