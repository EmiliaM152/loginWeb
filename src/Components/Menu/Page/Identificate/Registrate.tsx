import css from './Registrate.module.css'
export default function Registrate(){
    return(
        <div className={css["Contenido"]}>
            <h3>Registro de Usuario</h3>
            <div className={css["ContenidoFormulario"]}>
                <form>
                    <div className={css["Formulario"]}>
                        <div className={css["LdlUsuario"]}>
                            <label htmlFor='TxtUsuario'>Usuario:</label>
                        </div>
                            
                        <div className={css["TxtUsuario"]}>
                            <input name='TxtUsuario' type="text"/>
                        </div>
                        <div className={css["lblPassword1"]}>
                            <label>Password:</label>
                        </div>
                        <div className={css["TxtPassword1"]}>
                            <input type='password'/>
                        </div>
                        <div className={css["LblPassword2"]}>
                            <label>Password:</label>
                        </div>
                        <div className={css["TxtPassword2"]}>
                            <input type='password'/>
                        </div>
                        <div className={css["Botones"]}>
                        <button type='submit'>Aceptar</button>
                        <button type='button'>Cancelar</button>
                        </div>
                        

                       
                        
                       
                    </div>
                </form>
            </div>
        </div>
    )
}