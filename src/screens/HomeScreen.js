import React from 'react';
import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import { useSelector } from 'react-redux';

const HomeScreen = () => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const { user } = useSelector(state => state.auth);

  return (
    <div className="container">
      <h1>{t('welcome', { name: user ? user.name : 'Guest' })}</h1>
      <button onClick={() => navigate('/game')}>{t('startGame')}</button>
      <button onClick={() => navigate('/profile')}>{t('viewProfile')}</button>
      <button onClick={() => navigate('/leaderboard')}>{t('viewLeaderboard')}</button>
    </div>
  );
};

export default HomeScreen;