const initialState = {
  questions: [],
  currentQuestion: null,
  score: 0,
};

const gameReducer = (state = initialState, action) => {
  switch (action.type) {
    case 'SET_QUESTIONS':
      return {
        ...state,
        questions: action.payload,
        currentQuestion: action.payload[0],
      };
    case 'ANSWER_QUESTION':
      return {
        ...state,
        score: state.score + (action.payload.correct ? 1 : 0),
        currentQuestion: state.questions[state.questions.indexOf(state.currentQuestion) + 1],
      };
    default:
      return state;
  }
};

export default gameReducer;