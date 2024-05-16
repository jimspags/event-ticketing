export class EventModel {
  /**
   *
   */
  constructor(
    id: string,
    title: string,
    description: string,
    price: number,
    location: string,
    date: string
  ) {
    this.Id = id;
    this.Title = title;
    this.Description = description;
    this.Price = price;
    this.Location = location;
    this.Date = date;
  }

  Id!: string;
  Title!: string;
  Description!: string;
  Price!: number;
  Location!: string;
  Date!: string;
}
