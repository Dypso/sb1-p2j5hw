import { useEffect, useRef, useState } from 'react';
import io from 'socket.io-client';
import { WS_URL } from '../config';

export const useWebSocket = () => {
  const [isConnected, setIsConnected] = useState(false);
  const [lastMessage, setLastMessage] = useState(null);
  const socketRef = useRef(null);

  useEffect(() => {
    socketRef.current = io(WS_URL);

    socketRef.current.on('connect', () => setIsConnected(true));
    socketRef.current.on('disconnect', () => setIsConnected(false));
    socketRef.current.on('message', (message) => setLastMessage(message));

    return () => {
      socketRef.current.disconnect();
    };
  }, []);

  const sendMessage = (message) => {
    if (socketRef.current) {
      socketRef.current.emit('message', message);
    }
  };

  return { isConnected, lastMessage, sendMessage };
};