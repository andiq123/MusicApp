import { IPlaystate } from "./interfaces/IPlaystate.interface";

export class Playstate implements IPlaystate {
  playing: boolean = false;
  paused: boolean = false;
  stopped: boolean = true;
}
