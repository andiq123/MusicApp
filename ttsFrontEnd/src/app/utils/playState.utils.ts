export class playState {
  static playing = {
    playing: true,
    paused: false,
    stopped: false
  };
  static paused = {
    playing: false,
    paused: true,
    stopped: false
  };
  static stopped = {
    playing: false,
    paused: false,
    stopped: true
  };
}
