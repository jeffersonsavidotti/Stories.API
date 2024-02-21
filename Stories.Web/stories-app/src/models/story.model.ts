export class Story {
    id?: number;
    title: string;
    description: string;
    department: string;
    positiveVotesCount?: number;
    negativeVotesCount?: number;
  
    constructor(title: string, description: string, department: string) {
      this.title = title;
      this.description = description;
      this.department = department;
    }
  }
  