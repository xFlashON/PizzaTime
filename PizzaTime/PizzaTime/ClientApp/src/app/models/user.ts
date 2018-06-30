export class User {

  Id: string;
  Name: string;
  Email: string;
  PhoneNumber: string;
  DefaultDeliveryAdress: string;

  constructor(name: string, email: string, phoneNumber?: string, deliveryAdress?: string, id?: string) {

    this.Name = name;
    this.Email = email;
    this.PhoneNumber = phoneNumber ? phoneNumber : "";
    this.DefaultDeliveryAdress = deliveryAdress ? deliveryAdress : "";
    this.Id = id ? id : "";

  }

}
