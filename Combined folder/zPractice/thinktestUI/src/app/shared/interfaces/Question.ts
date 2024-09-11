export interface Question {
    id: number;
    question: string;
    type: 'single' | 'multiple';
    options: string[];
    answer: number | number[];
  }
  