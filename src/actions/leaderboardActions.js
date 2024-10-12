export const fetchLeaderboard = () => {
  // This should be an async action using redux-thunk
  return async (dispatch) => {
    // Simulating API call
    const leaderboard = [
      { id: 1, name: "Alice", score: 100 },
      { id: 2, name: "Bob", score: 90 },
      { id: 3, name: "Charlie", score: 80 },
    ];
    dispatch({ type: 'SET_LEADERBOARD', payload: leaderboard });
  };
};