import { TechniqueType } from "./TechniqueTypeModel";

export class TechniqueTypeList {
    techniqueList: TechniqueType[] = [];

    public constructor() {
        this.techniqueList.push(new TechniqueType("assets/type1.png", "Автовышки", "Автовышка"));
        this.techniqueList.push(new TechniqueType("assets/type2.png", "Автокраны", "Автокран"));
        this.techniqueList.push(new TechniqueType("assets/type3.png", "Бетононасосы", "Бетононасос"));
        this.techniqueList.push(new TechniqueType("assets/type4.png", "Бульдозеры", "Бульдозер"));
        this.techniqueList.push(new TechniqueType("assets/type5.png", "Гидромолоты", "Гидромолот"));
        this.techniqueList.push(new TechniqueType("assets/type6.png", "Грунторезы", "Грунторез"));
        this.techniqueList.push(new TechniqueType("assets/type7.png", "Компрессоры", "Компрессор"));
        this.techniqueList.push(new TechniqueType("assets/type8.png", "Манипуляторы", "Манипулятор"));
        this.techniqueList.push(new TechniqueType("assets/type9.png", "Минипогрузчики", "Минипогрузчик"));
        this.techniqueList.push(new TechniqueType("assets/type10.png", "Полуприцепы", "Полуприцеп"));
        this.techniqueList.push(new TechniqueType("assets/type11.png", "Самосвалы", "Самосвал"));
        this.techniqueList.push(new TechniqueType("assets/type12.png", "Тралы", "Трал"));
        this.techniqueList.push(new TechniqueType("assets/type13.png", "Фронтальные погрузчики", "Погрузчик"));
        this.techniqueList.push(new TechniqueType("assets/type14.png", "Экскаваторы", "Экскаватор"));
        this.techniqueList.push(new TechniqueType("assets/type15.png", "Экскаваторы-погрузчики JCB", "JCB"));
        this.techniqueList.push(new TechniqueType("assets/type16.png", "Ямобуры", "Ямобур"));
    }
}