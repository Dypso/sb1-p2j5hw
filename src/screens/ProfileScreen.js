import React from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { useTranslation } from 'react-i18next';
import { logout } from '../actions/authActions';
import { useNavigate } from 'react-router-dom';

const ProfileScreen = () => {
  const { t } = useTranslation();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { user } = useSelector(state => state.auth);

  const handleLogout = () => {
    dispatch(logout());
    navigate('/');
  };

  return (
    <div className="container">
      <h1>{t('profile')}</h1>
      <p>{t('name', { name: user.name })}</p>
      <p>{t('email', { email: user.email })}</p>
      <p>{t('totalScore', { score: user.totalScore })}</p>
      <button onClick={handleLogout}>{t('logout')}</button>
    </div>
  );
};

export default ProfileScreen;