export default function appReducer(state, action) {
    switch (action.type) {
      case "ADD_JUEGO":
        return {
          ...state,
          juegos: [...state.juegos, action.payload],
        };
      case "UPDATE_JUEGO": {
        const updatedJuego = action.payload;
  
        const updatedJuego = state.juegos.map((juego) => {
          if (juego.id === updatedJuego.id) {
            updatedJuego.done = juego.done;
            return updatedJuego;
          }
          return juego;
        });
        return {
          ...state,
          juego: updatedJuego,
        };
      }
      case "DELETE_JUEGO":
        return {
          ...state,
          juegos: state.juegos.filter((juego) => juego.id !== action.payload),
        };
      case "TOGGLE_JUEGO_DONE":
        const updatedJuego = state.juegos.map((juego) => {
          if (juego.id === action.payload) {
            return { ...juego, done: !juego.done };
          }
          return juego;
        });
        return {
          ...state,
          juegos: updatedJuego,
        };
      default:
        return state;
    }
  }