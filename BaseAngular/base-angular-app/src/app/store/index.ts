import { ActionReducerMap, MetaReducer } from '@ngrx/store';
import { environment } from '../../environments/environment';
import { homeReducer, HomeState } from '../features/home/store';

/**
 * Estado global de la aplicación
 * Aquí se agregan los reducers de cada feature
 */
export interface AppState {
  home: HomeState;
  // Agregar aquí los estados de otros features
  // ejemplo: auth: AuthState;
}

/**
 * Mapa de reducers de la aplicación
 */
export const reducers: ActionReducerMap<AppState> = {
  home: homeReducer,
  // Agregar aquí otros reducers
  // ejemplo: auth: authReducer
};

/**
 * Meta-reducers para logging y desarrollo
 */
export const metaReducers: MetaReducer<AppState>[] = !environment.production
  ? []
  : [];
