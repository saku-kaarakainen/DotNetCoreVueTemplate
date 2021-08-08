// 2021-08-07: Class copied and modified from: https://github.com/SoftwareAteliers/asp-net-core-vue-starter/blob/master/ClientApp/src/models/Forecast.ts
// Credits to SoftwareAteliers
export class Forecast {
  constructor(date?: Date, temperatureC?: number, temperatureF?: number, summary?: string) {
    this.date = date
    this.temperatureC = temperatureC
    this.temperatureF = temperatureF
    this.summary = summary
  }

  public date?: Date
  public temperatureC?: number
  public temperatureF?: number
  public summary?: string
}
