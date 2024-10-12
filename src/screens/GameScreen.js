import React, { useState, useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { fetchQuestions, submitAnswer } from '../actions/gameActions';
import { useWebSocket } from '../hooks/useWebSocket';

const GameScreen = () => {
  const { t } = useTranslation();
  const dispatch = useDispatch();
  const { questions, currentQuestion, score } = useSelector(state => state.game);
  const { isConnected, lastMessage, sendMessage } = useWebSocket();

  useEffect(() => {
    dispatch(fetchQuestions());
  }, [dispatch]);

  useEffect(() => {
    if (lastMessage) {
      console.log('Received WebSocket message:', lastMessage);
    }
  }, [lastMessage]);

  const handleAnswer = (selectedAnswer) => {
    dispatch(submitAnswer(currentQuestion.id, selectedAnswer));
    if (isConnected) {
      sendMessage({ type: 'SUBMIT_ANSWER', payload: { questionId: currentQuestion.id, answer: selectedAnswer } });
    }
  };

  if (!currentQuestion) {
    return <div>{t('loading')}</div>;
  }

  return (
    <div className="container">
      <h2>{currentQuestion.text}</h2>
      {currentQuestion.options.map((option, index) => (
        <button key={index} onClick={() => handleAnswer(option)}>
          {option}
        </button>
      ))}
      <p>{t('score', { score })}</p>
    </div>
  );
};

export default GameScreen;