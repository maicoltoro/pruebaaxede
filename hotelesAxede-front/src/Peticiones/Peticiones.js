import { Alojamiento, Solicitudes, sedes, temportada } from "../Constantes/Conts";

export const TraerAlojamientos = async (sede)=>{

    const solicitud = await fetch(`${Alojamiento}/${sede}`,{
        method:'GET',
    })
    const respuesta = await solicitud.json()
    return respuesta;
}

export const TraerSolicitudes = async ()=>{

    const solicitud = await fetch(`${Solicitudes}`,{
        method:'GET',
    })
    const respuesta = await solicitud.json()
    return respuesta;
}

export const eliminar= async (id) =>{
    const solicitud = await fetch(`${Solicitudes}/${id}`,{
        method:'delete',
    })
    return solicitud.status;
}

export const ListarSedes= async () =>{
    const solicitud = await fetch(`${sedes}`,{
        method:'get',
    })
    const respuesta = await solicitud.json();
    return respuesta;
}

export const ListarTemporada = async () =>{
    const solicitud = await fetch(`${temportada}`,{
        method:'get',
    })
    const respuesta = await solicitud.json();
    return respuesta;
}

export const GuardareSolicitudes = async ( data)=>{

    const solicitud = await fetch(`${Solicitudes}`,{
        method:'POST',
        headers:{
            'Content-Type': 'application/json',
        },
        body : JSON.stringify(data)
    })
    return solicitud.status
}