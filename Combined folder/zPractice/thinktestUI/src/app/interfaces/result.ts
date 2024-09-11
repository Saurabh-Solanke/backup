export interface IResultData
{
    subjectName             : string,
    userId                  : number,
    subjectId               : number,
    totalQuestions          : number,
    attemptedQuestions      : number,
    unAttemptedQuestions    : number,
    correctQuestions        : number,
    inCorrectQuestions      : number,
    percentageObtained      : number,
}

export interface IUserResultData
{
    totalQuestions          : number,
    attemptedQuestions      : number,
    unAttemptedQuestions    : number,
    correctQuestions        : number,
    inCorrectQuestions      : number,
    percentageObtained      : number,
    subjectName             : string,

}