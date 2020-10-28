
import { Action } from '@ngrx/store';
import { Message } from '../models/message';

export function addMessageReducer(state: Message[] = [], action) {
  switch (action.type) {
    case 'LOG_MESSAGE':
        return [...state, action.payload];
    default:
        return state;
    }
}
