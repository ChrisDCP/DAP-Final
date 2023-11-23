import { useContext, useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { GlobalContext } from "../context/globalState";

const JuegoForm = () => {
  const [juego, setJuego] = useState({
    id: "",
    nombre: "",
    descripcion: "",
    fechaLanzamiento:"",
    compañiaId:""
  });
  const { addJuego, updateJuego, juegos } = useContext(GlobalContext);

  const navigate = useNavigate();
  const params = useParams();

  const handleChange = (e) =>
    setJuego({ ...juego, [e.target.name]: e.target.value });

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!juego.id) {
      addJuego(juego);
    } else {
      updateJuego(juego);
    }
    navigate("/");
  };

  useEffect(() => {
    const JuegoFound = juego.find((juego) => juego.id === params.id);
    if (JuegoFound) {
      setTask({
        id: JuegoFound.id,
        nombre: JuegoFound.title,
        descripcion: JuegoFound.descripcion,
        fechaLanzamiento:JuegoFound.fechaLanzamiento,
        compañiaId:JuegoFound.compañiaId
      });
    }
  }, [params.id, juegos]);

  return (
    <div className="flex justify-center items-center h-3/4">
      <form onSubmit={handleSubmit} className="bg-gray-900 p-10">
        <h2 className="text-3xl mb-7">
          {juego.id ? "Update " : "Create "}Un Juego
        </h2>
        <div className="mb-5">
          <input
            type="text"
            name="title"
            value={juego.nombre}
            onChange={handleChange}
            placeholder="Write a title"
            className="py-3 px-4 focus:outline-none focus:text-gray-100 bg-gray-700 w-full"
            autoFocus
          />
        </div>
        <div className="mb-5">
          <textarea
            value={juego.descripcion}
            name="descripcion"
            rows="2"
            placeholder="write a description"
            onChange={handleChange}
            className="py-3 px-4 focus:outline-none focus:text-gray-100 bg-gray-700 w-full"
          ></textarea>
          <button className="bg-green-600 w-full hover:bg-green-500 py-2 px-4 mt-5">
            {juego.id ? "Update Juego" : "Create Juego"}
          </button>
        </div>
      </form>
    </div>
  );
};

export default TaskForm;