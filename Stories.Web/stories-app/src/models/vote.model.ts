export class Vote {
    id?: number;
    idStory: number;
    idUser: number;
    voteValue: boolean; // true para positivo, false para negativo
  
    constructor(idStory: number, idUser: number, voteValue: boolean) {
      this.idStory = idStory;
      this.idUser = idUser;
      this.voteValue = voteValue;
    }
  }
  