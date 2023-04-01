import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { MailService } from 'src/app/services/mail.service';
import { MailModel } from 'src/app/models/MailModel';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.scss']
})
export class CompanyComponent implements OnInit {

  public name: string | undefined;
  public invalidName: boolean = false;
  public messageName: string | undefined;
  public phone: string | undefined;
  public invalidPhone: boolean = false;
  public messagePhone: string | undefined;
  public email: string | undefined;
  public invalidEmail: boolean = false;
  public messageEmail: string | undefined;
  public description: string = "";

  constructor(private accountService: AccountService, private mailService: MailService) { }

  public resetValidFlag(): boolean {
    return false;
  }

  private validateForm(): boolean {
    let valid = true;
    let toScroll = true;
    if (this.name == undefined || this.name.trim() == '') {
      this.messageName = "Введите ваше ФИО";
      this.invalidName = true;
      this.name = '';
      valid = false;
      if (toScroll) {
        document.getElementById("name")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.email == undefined || this.email.trim() == '') {
      this.messageEmail = "Введите вашу электронную почту";
      this.invalidEmail = true;
      this.email = '';
      valid = false;
      if (toScroll) {
        document.getElementById("email")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    if (this.phone == undefined || this.phone.trim() == '') {
      this.messagePhone = "Введите ваш номер телефона";
      this.invalidPhone = true;
      this.phone = '';
      valid = false;
      if (toScroll) {
        document.getElementById("phone")?.scrollIntoView({ block: "center", inline: "center", behavior: "smooth" });
        toScroll = false;
      }
    }
    return valid;
  }

  public async sendMail(): Promise<void> {
    if (!this.validateForm())
      return;
    if (this.name == undefined || this.email == undefined || this.phone == undefined)
      return;
    const mail = new MailModel(this.name, this.email, this.phone, this.description);
    await this.mailService.sendMail(mail)
      .then(
        (data) => {
          console.log(data);
          alert(data);
          location.reload();
          return;
        }
      )
      .catch(
        (error) => {
          alert("Ошибка отправки формы");
          console.log(error);
          this.name = '';
          this.phone = '';
          this.email = '';
          this.description = '';
          return;
        }
      )
  }

  public async ngOnInit(): Promise<void> {
    await this.accountService.getAuthoriseModel();
  }

}
