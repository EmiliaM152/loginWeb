
import logo from "./Logo.jpg";
import ususrios from "./Login.jpg"
import Menu from"./Components/Menu/Menu"
function App() {
  return (
    <div className="container">
      <div className="header"> 
         <div className="Logo">
          <img src={logo} alt='' width={100} height={100}/>
          </div>
         <div className="titulo"> Web App</div>
         <div className="usuario">
          <img src={ususrios} alt='' width={60} height={60}/>
          <div>Invitados</div>
          </div>
      </div>
          <div className="navbar"><Menu/></div>
          <div className="main">este contenifo de opcion</div>
          <div className="footer">@Emilia.Todos los derechos</div>
    </div>
  );
}

export default App;
