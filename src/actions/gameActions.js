export const fetchQuestions = () => {
  // This should be an async action using redux-thunk
  return async (dispatch) => {
    // Simulating API call
    const questions = [
      { id: 1, text: "Quelle est la capitale de la France ?", options: ["Paris", "Londres", "Berlin", "Madrid"] },
      { id: 2, text: "Quel est le plus grand océan du monde ?", options: ["Atlantique", "Indien", "Arctique", "Pacifique"] },
    ];
    dispatch({ type: 'SET_QUESTIONS', payload: questions });
  };
};

export const submitAnswer = (questionId, answer) => ({
  type: 'ANSWER_QUESTION',
  payload: { questionId, answer, correct: true }, // You should implement the logic to check if the answer is correct
});