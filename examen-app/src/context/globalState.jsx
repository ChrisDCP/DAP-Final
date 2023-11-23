import { createContext, useReducer } from "react";
import { v4 } from "uuid";

import appReducer from "./AppReducer";

const initialState = {
  juegos: [
    {
      id: "1",
      title: "some title",
      description: "some description",
      fechaLanzamiento: "some date",
      compañiaId:"some id",
      done: false,
    },
    {
      id: "2",
      title: "some title",
      description: "some description",
      fechaLanzamiento: "some date",
      compañiaId:"some id",
      done: false,
    },
  ],
};

export const GlobalContext = createContext(initialState);

export const GlobalProvider = ({ children }) => {
  const [state, dispatch] = useReducer(appReducer, initialState);

  function addJuego(juego) {
    dispatch({
      type: "ADD_JUEGO",
      payload: { ...juego, id: v4(), done: false },
    });
  }

  function updateJuego(updatedJuego) {
    dispatch({
      type: "UPDATE_JUEGO",
      payload: updatedJuego,
    });
  }

  function deleteJuego(id) {
    dispatch({
      type: "DELETE_JUEGO",
      payload: id,
    });
  }

  function toggleTaskDone(id) {
    dispatch({
      type: "TOGGLE_JUEGO_DONE",
      payload: id,
    });
  }

  return (
    <GlobalContext.Provider
      value={{
        juegos: state.juegos,
        addJuego,
        updateJuego,
        deleteJuego,
        toggleTaskDone
      }}
    >
      {children}
    </GlobalContext.Provider>
  );
};