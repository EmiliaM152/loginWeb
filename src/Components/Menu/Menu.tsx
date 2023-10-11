import css from "./Menu.module.css";

export default function Menu() {
    return (
        <nav>
            <div className={css["option-container"]} >
                <div className={css["option"]}>
                    Inicio
                </div>
                <div className={css["separator"]}></div>

                <div className={css["option"]}>
                    Identificate
                </div>
                <div className={css["option"]}>
                    Registrate
                </div>
            </div>
        </nav>
    )
}
