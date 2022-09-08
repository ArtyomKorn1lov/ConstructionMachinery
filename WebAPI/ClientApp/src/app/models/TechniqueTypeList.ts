import { TechniqueType } from "./TechniqueTypeModel";

export class TechniqueTypeList {
    techniqueList: TechniqueType[] = [];

    public constructor() {
        this.techniqueList.push(new TechniqueType("assets/type1.png", "Автовышки", "Автовышка", "assets/type2.png", "Автокраны", "Автокран"));
        this.techniqueList.push(new TechniqueType("assets/type3.png", "Бетононасосы", "Бетононасос", "assets/type4.png", "Бульдозеры", "Бульдозер"));
        this.techniqueList.push(new TechniqueType("assets/type5.png", "Гидромолоты", "Гидромолот", "assets/type6.png", "Грунторезы", "Грунторез"));
        this.techniqueList.push(new TechniqueType("assets/type7.png", "Компрессоры", "Компрессор", "assets/type8.png", "Манипуляторы", "Манипулятор"));
        this.techniqueList.push(new TechniqueType("assets/type9.png", "Минипогрузчики", "Минипогрузчик", "assets/type10.png", "Полуприцепы", "Полуприцеп"));
        this.techniqueList.push(new TechniqueType("assets/type11.png", "Самосвалы", "Самосвал", "assets/type12.png", "Тралы", "Трал"));
        this.techniqueList.push(new TechniqueType("assets/type13.png", "Фронтальные погрузчики", "Погрузчик", "assets/type14.png", "Экскаваторы", "Экскаватор"));
        this.techniqueList.push(new TechniqueType("assets/type15.png", "Экскаваторы-погрузчики JCB", "JCB", "assets/type16.png", "Ямобуры", "Ямобур"));
    }
}