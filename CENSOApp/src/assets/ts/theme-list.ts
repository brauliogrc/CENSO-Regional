import { Theme } from './interfaces/newInterfaces';
class NewTheme {
  themeId: number;
  name: number;

  static newTheme({ tId, tName }): NewTheme {
    return new NewTheme(tId, tName);
  }

  constructor(themeId: number, name: number) {
    this.themeId = themeId;
    this.name = name;
  }
}

export class ThemeList {
  private themeList = {};

  constructor() {
    this.themeList = {};
  }

  get getThemeList(): Theme[] {
    const list: Theme[] = [];
    Object.keys(this.themeList).forEach((key) => {
      const theme = this.themeList[key];
      list.push(theme);
    });

    return list;
  }

  addNewTheme(theme: any): void {
    const newTheme = NewTheme.newTheme(theme);
    this.themeList[newTheme.themeId] = newTheme;
  }
}
