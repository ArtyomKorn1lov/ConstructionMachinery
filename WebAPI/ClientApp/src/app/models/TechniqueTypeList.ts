import { TechniqueType } from "./TechniqueTypeModel";

export class TechniqueTypeList {
    techniqueList: TechniqueType[] = [];

    public constructor() {
        this.techniqueList.push(new TechniqueType("assets/type1.png", "Автовышки", "assets/type2.png", "Автокраны"));
        this.techniqueList.push(new TechniqueType("assets/type3.png", "Бетононасосы", "assets/type4.png", "Бульдозеры"));
        this.techniqueList.push(new TechniqueType("assets/type5.png", "Гидромолоты", "assets/type6.png", "Грунторезы"));
        this.techniqueList.push(new TechniqueType("assets/type7.png", "Компрессоры", "assets/type8.png", "Манипуляторы"));
        this.techniqueList.push(new TechniqueType("assets/type9.png", "Минипогрузчики", "assets/type10.png", "Полуприцепы"));
        this.techniqueList.push(new TechniqueType("assets/type11.png", "Самосвалы", "assets/type12.png", "Тралы"));
        this.techniqueList.push(new TechniqueType("assets/type13.png", "Фронтальные погрузчики", "assets/type14.png", "Экскаваторы"));
        this.techniqueList.push(new TechniqueType("assets/type15.png", "Экскаваторы-погрузчики JCB", "assets/type16.png", "Ямобуры"));
    }
}