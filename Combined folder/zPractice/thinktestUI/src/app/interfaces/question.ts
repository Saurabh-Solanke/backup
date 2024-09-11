export interface Question {
    id: string;
    question: string;
    type: 'single' | 'multiple';
    options: string[];
    answer: number | number[];
  }