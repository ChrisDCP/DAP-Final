import axios from "axios";
const apiUrl = "http://localhost:5101/api/Juego/list";

console.log (apiUrl);

const [data, setData] = useState([]);
console.log(data);
useEffect(() => {
    const fetchData = async () => {
        
      await axios.get('http://localhost:5101/api/Juego/list')
        .then(res => {
          setData(res.data);
          console.log(data);
         // setInfo(res.data.info);
        })
        .catch(err => {
          console.log(err);
        });
    };
    fetchData();
  }, []);

export const getAdministrador = () => {
  const baseUrl = `${apiUrl}`;
  return fetch(baseUrl)
    .then((res) => res.json())
    .then((response) => {
      const data = ([] = [...response]);
      console.log(data);
      return data;
    })
    .catch((error) => {});
};
export async function getAdministradorByID(correo, contraseña) {
  const baseUrl = `${apiUrl}/Seleccionar/${correo}/${contraseña}`;
  return await fetch(baseUrl)
    .then((res) => res.json())
    .then((response) => {
      const data = response;
      return data;
    })
    .catch((error) => {
      return undefined;
    });
}
function request(product, method) {
  const requestOptions = {
    method,
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(product),
  };
  return requestOptions;
}
export async function postAdministrador(Administrador) {
  const baseUrl = apiUrl;
  await axios
    .post(baseUrl, Administrador)
    .then((response) => console.log("success full"))
    .catch((error) => console.log(error));
}
export async function putAdministrador(Administrador = {}, ID) {
  const baseUrl = `${apiUrl}/${ID}`;
  const response = await fetch(baseUrl, {
    method: "PUT",
    mode: "cors",
    cache: "no-cache",
    credentials: "same-origin",
    headers: {
      "Content-Type": "application/json",
    },
    redirect: "follow",
    referrerPolicy: "no-referrer",
    body: JSON.stringify(Administrador),
  });
  return response.json();
}
export async function deleteAdministrador(ID) {
  const baseUrl = `${apiUrl}/${ID}`;
  const response = await fetch(baseUrl, {
    method: "DELETE",
  });
  return response.json();
}

export const fetchAdministrador = {
  getAdministrador,
  postAdministrador,
  putAdministrador,
  deleteAdministrador,
  getAdministradorByID,
};
