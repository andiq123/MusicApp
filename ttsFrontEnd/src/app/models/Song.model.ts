import { ISong } from "./interfaces/ISong.interface";
import { IPlaystate } from "./interfaces/IPlaystate.interface";

export class Song implements ISong {
  name: string;
  artist?: string;
  album?: string;
  url?: string;
  cover_art_url?: string;
  playstatus?: IPlaystate;
  loading?: boolean;
  loadingValue?: number;
  error?: boolean = false;
}
