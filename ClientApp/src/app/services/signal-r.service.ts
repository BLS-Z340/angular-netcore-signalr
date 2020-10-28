import { Injectable } from "@angular/core";
import { HubConnection, HubConnectionBuilder } from "@aspnet/signalr";
import { Store } from "@ngrx/store";
import { AppState } from "../app.state";
import { Message } from "../models/message";

@Injectable({
  providedIn: "root",
})
export class SignalRService {
  private hubConnection: HubConnection;

  constructor(private store: Store<AppState>) {}

  addMessage(type, payload) {
    this.store.dispatch({
      type: "LOG_MESSAGE",
      payload: <Message>{
        type: type,
        payload: payload,
      },
    });
  }

  public startConnection() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl("/example-hub")
      .build();
    this.hubConnection
      .start()
      .then(() => console.log("Connection started!"))
      .catch((err) => console.log("Error while establishing connection"));

    this.hubConnection.on(
      "BroadcastMessage",
      (type: string, payload: string) => {
        console.log(
          "Message from signalir! " + { severity: type, summary: payload }
        );
        this.addMessage(type, payload);
      }
    );
  }
}
