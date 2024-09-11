export interface ISub{
  subjectId          : number;
    subjectName        : string;
}

export interface ISubject {
    id          : number;
    name        : string;
    questions   : IQuestion[];
  }
  

  export interface IQuestion {
    id          : number;
    question    : string;
    options     : string[];
    answer      : number[]; // Index of the correct option
    isMultiple  : boolean
  }


  export interface IQuestionResponse {
    questionId      : number;
    selectedOptions : number[];
  }
  
  export interface IExamSubmission {
    responses     : IQuestionResponse[];
  }
  
  export interface IExamResult {
    totalQuestions: number;
    attempted     : number;
    unattempted   : number;
    correct       : number;
    incorrect     : number;
    percentage    : number;
  }
  