export class User {
    id?: number; // O "?" indica que o campo é opcional, útil para criações onde o ID ainda não existe
    name: string;
    positiveVotesCount?: number; // Opcional, dependendo de como você quer lidar com votos
    negativeVotesCount?: number; // Opcional, dependendo de como você quer lidar com votos
  
    constructor(name: string) {
      this.name = name;
    }
  }
  