import React, {useEffect, useState} from "react";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import axios from "axios";
import { show_alerta } from "../function";


const Juego = () => {
  const url = 'http://localhost:5101/api/Juego';
  const [juegos,setJuegos]=useState(['']);
  const [id,SetId]=useState(['']);
  const [nombre,SetNombre]=useState(['']);
  const [descripcion,setDescripcion]=useState(['']);
  const [fechaLanzamiento, setFechaLanzamiento]=useState(['']);
  const [compañiaid,setCompañiaId]=useState(['']);

  useEffect( ()=>{
    getJuegos();
  })

  const getJuegos = async () =>{
    const respuesta = await axios.get(url);
    setJuegos(respuesta.data);
  }

  return(
    <div className='App'>
      <div clasName= 'container-fluid'>
        <div className='row mt-3'>
          <div className='col-md-4- offset-md-4'>
            <div className='d-grid- mx-auto'>
              <button className='btn btn-dark' data-bs-toggle='modal' data-bs-target='#modalJuegos'>
                <i className='fa-solid fa-circle-plus'></i> Añadir
              </button>
            </div>
          </div>
        </div>
        <div className='row mt-3'>
          <div className='col-12 col-lg-8 offset-0 offset-lg-2'>
            <div className='table-responsive'>
              <table className='table table-bordered'>
                <thead>
                  <tr><th>#</th><th>JUEGOS</th><th>DESCRIPCION</th><th>FECHA LANZAMIENTO</th></tr>
                </thead>
                <tbody className='table-group-divider'>
                  {juegos.map((Juego,i)=>(
                      <tr key={Juego.id}>
                        <td>{(i+1)}</td>
                        <td>{Juego.nombre}</td>

                      </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
      <div classname='modal fade'>

      </div>
    </div>
  )

}

export default Juego;

