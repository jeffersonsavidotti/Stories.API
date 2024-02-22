export class User {
    id?: number;
    name: string;
    positiveVotesCount?: number;
    negativeVotesCount?: number;
  
    constructor(name: string) {
      this.name = name;
    }
  }
  