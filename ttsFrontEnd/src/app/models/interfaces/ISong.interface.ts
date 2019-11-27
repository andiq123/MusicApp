import { IPlaystate } from './IPlaystate.interface';

export interface ISong {
  name: string;
  artist?: string;
  album?: string;
  url?: string;
  cover_art_url?: string;
  playstatus?: IPlaystate;
  loading?: boolean;
  error?: boolean;
}
