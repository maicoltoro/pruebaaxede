import React, { useState, useEffect, useRef } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Toast } from 'primereact/toast';
import { Button } from 'primereact/button';
import { Toolbar } from 'primereact/toolbar';
import { IconField } from 'primereact/iconfield';
import { InputIcon } from 'primereact/inputicon';
import { InputNumber } from 'primereact/inputnumber';
import { Dialog } from 'primereact/dialog';
import { InputText } from 'primereact/inputtext';
import { GuardareSolicitudes, ListarSedes, ListarTemporada, TraerAlojamientos, TraerSolicitudes, eliminar } from '../Peticiones/Peticiones';
import { Dropdown } from 'primereact/dropdown';
import { emptyProduct } from '../Constantes/Conts';

export default function Solicitudes() {
    

    const [products, setProducts] = useState(null);
    const [productDialog, setProductDialog] = useState(false);
    const [deleteProductDialog, setDeleteProductDialog] = useState(false);
    const [deleteProductsDialog, setDeleteProductsDialog] = useState(false);
    const [product, setProduct] = useState(emptyProduct);
    const [selectedProducts, setSelectedProducts] = useState(null);
    const [submitted, setSubmitted] = useState(false);
    const [sede, setSede] = useState(null)
    const [selectSede, setSelectSede] = useState(null) 
    const [selectalojamiento, setSelectAlojamiento] = useState(null) 
    const [selectTemporada, setSelectTemporada] = useState(null) 
    const [alojamiento, setalojamineto] = useState() 
    const [temporada, setTemporada] = useState() 
    const [globalFilter, setGlobalFilter] = useState(null);
    const toast = useRef(null);
    const dt = useRef(null);

    useEffect(() => {
        const Soli = async () =>{
            const solicitid = await TraerSolicitudes()
            setProducts(solicitid)
            const sedes = await ListarSedes()
            setSede(sedes)
        }
        Soli()
    }, []);

    const ListarAlojamiento = async (event) =>{
        
        setSelectSede(event.value);
        const newAlojamiento = await TraerAlojamientos(event.value.sede1)
        setSelectAlojamiento(newAlojamiento)
        const newTemporada = await ListarTemporada()
        setSelectTemporada(newTemporada)
    }

    const openNew = () => {
        setProduct(emptyProduct);
        setSubmitted(false);
        setProductDialog(true);
    };

    const hideDialog = () => {
        setSubmitted(false);
        setProductDialog(false);
    };

    const hideDeleteProductDialog = () => {
        setDeleteProductDialog(false);
    };

    const hideDeleteProductsDialog = () => {
        setDeleteProductsDialog(false);
    };

    const saveProduct = async () => {
        setSubmitted(true);
        if (product.sede.sede1.trim()) {

            const newSolicitud = {
                IdAlojamiento : product.alojamiento.id,
                IdSede : product.sede.id,
                CantidadHabitaciones : product.cantidadHabitaciones,
                IdTemporada : product.temporada.id,
                cantidadPersonas : product.cantidadPersonas
            }
            const guardarsolicitud = await GuardareSolicitudes(newSolicitud)
            if(guardarsolicitud === 200){
                let _products = [...products];
                let _product = { ...product };

                _product.id = createId();
                _product.alojamiento = _product.alojamiento.alojamiento
                _product.sede = _product.sede.sede1
                _product.temporada = _product.temporada.temporada
                _products.push(_product);
                toast.current.show({ severity: 'success', summary: 'Successful', detail: 'Product Created', life: 3000 });


                setProducts(_products);
                setProductDialog(false);
                setProduct(emptyProduct);
            }
        }
    };

    const confirmDeleteProduct = (product) => {
        setProduct(product);
        setDeleteProductDialog(true);
    };

    const deleteProduct = async () => {
        let _products = products.filter((val) => val.id !== product.id);
        const SolicitudEliminar = await eliminar(product.id)
        if(SolicitudEliminar === 200){
            setProducts(_products);
            setDeleteProductDialog(false);
            setProduct(emptyProduct);
            toast.current.show({ severity: 'success', summary: 'Successful', detail: 'Product Deleted', life: 3000 });
        }
    };

    const createId = () => {
        const id = products[products.length - 1].id + 1
        return id;
    };

    const onInputNumberChange = (e, name) => {
        const val = e.value || 0;
        let _product = { ...product };

        _product[`${name}`] = val;

        setProduct(_product);
    };

    const leftToolbarTemplate = () => {
        return (
            <div className="flex flex-wrap gap-2">
                <Button label="New" icon="pi pi-plus" severity="success" onClick={openNew} />
            </div>
        );
    };

    const actionBodyTemplate = (rowData) => {
        return (
            <React.Fragment>
                <Button icon="pi pi-trash" rounded outlined severity="danger" onClick={() => confirmDeleteProduct(rowData)} />
            </React.Fragment>
        );
    };

    const header = (
        <div className="flex flex-wrap gap-2 align-items-center justify-content-between">
            <h4 className="m-0">Manage Products</h4>
            <IconField iconPosition="left">
                <InputIcon className="pi pi-search" />
                <InputText type="search" onInput={(e) => setGlobalFilter(e.target.value)} placeholder="Search..." />
            </IconField>
        </div>
    );
    const productDialogFooter = (
        <React.Fragment>
            <Button label="Cancel" icon="pi pi-times" outlined onClick={hideDialog} />
            <Button label="Save" icon="pi pi-check" onClick={saveProduct} />
        </React.Fragment>
    );
    const deleteProductDialogFooter = (
        <React.Fragment>
            <Button label="No" icon="pi pi-times" outlined onClick={hideDeleteProductDialog} />
            <Button label="Yes" icon="pi pi-check" severity="danger" onClick={deleteProduct} />
        </React.Fragment>
    );
    const deleteProductsDialogFooter = (
        <React.Fragment>
            <Button label="No" icon="pi pi-times" outlined onClick={hideDeleteProductsDialog} />
        </React.Fragment>
    );

    return (
        <div>
            <Toast ref={toast} />
            <div className="card">
                <Toolbar className="mb-4" left={leftToolbarTemplate}></Toolbar>

                <DataTable ref={dt} value={products} selection={selectedProducts} onSelectionChange={(e) => setSelectedProducts(e.value)}
                        dataKey="id"  paginator rows={10} rowsPerPageOptions={[5, 10, 25]}
                        paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
                        currentPageReportTemplate="Showing {first} to {last} of {totalRecords} products" globalFilter={globalFilter} header={header}>
                    <Column selectionMode="multiple" exportable={false}></Column>
                    <Column field="id" header="Id" sortable style={{ minWidth: '12rem' }}></Column>
                    <Column field="sede" header="sede" sortable style={{ minWidth: '16rem' }}></Column>
                    <Column field="alojamiento" header="alojamiento" sortable style={{ minWidth: '10rem' }}></Column>
                    <Column field="cantidadHabitaciones" header="Habitaciones" sortable style={{ minWidth: '10rem' }}></Column>
                    <Column field="cantidadPersonas" header="Personas" sortable style={{ minWidth: '10rem' }}></Column>
                    <Column field="temporada" header="temporada" sortable style={{ minWidth: '10rem' }}></Column>
                    <Column body={actionBodyTemplate} exportable={false} style={{ minWidth: '12rem' }}></Column>
                </DataTable>
            </div>

            <Dialog visible={productDialog} style={{ width: '32rem' }} breakpoints={{ '960px': '75vw', '641px': '90vw' }} header="Product Details" modal className="p-fluid" footer={productDialogFooter} onHide={hideDialog}>

                <div className="formgrid grid">
                    <label htmlFor="price" className="font-bold">
                            Sede
                    </label>
                    <div className="card flex justify-content-center">
                        <Dropdown value={selectSede}  onChange={(e) => { ListarAlojamiento(e);  onInputNumberChange(e, 'sede')}} options={sede}  optionLabel="sede1" 
                            placeholder="Selecciona una sede" className="w-full md:w-14rem" />
                    </div>

                    <label htmlFor="price" className="font-bold">
                           alojamiento
                    </label>
                    <div className="card flex justify-content-center">
                        <Dropdown value={alojamiento}  onChange={(e) => {setalojamineto(e.value);  onInputNumberChange(e, 'alojamiento')}} options={selectalojamiento} optionLabel="alojamiento" 
                            placeholder="Selecciona un alojamineto" className="w-full md:w-14rem" />
                    </div>
                    
                    <label htmlFor="price" className="font-bold">
                            temporada
                    </label>
                    <div className="card flex justify-content-center">
                        <Dropdown value={temporada}  onChange={(e) => {setTemporada(e.value);  onInputNumberChange(e, 'temporada')}} options={selectTemporada}  optionLabel="temporada" 
                            placeholder="Selecciona una temporada" className="w-full md:w-14rem" />
                    </div>
                </div>

                <div className="formgrid grid">
                    <div className="field col">
                        <label htmlFor="price" className="font-bold">
                            Cantidad de personas
                        </label>
                        
                        <InputNumber id="personas" value={product.price} onChange={(e) => onInputNumberChange(e, 'cantidadPersonas')} />
                    </div>

                    <div className="field col">
                        <label htmlFor="quantity" className="font-bold">
                            Cantidad de Habitaciones
                        </label>
                        <InputNumber id="habitaciones" value={product.quantity} onChange={(e) => onInputNumberChange(e, 'cantidadHabitaciones')} />
                    </div>
                </div>
            </Dialog>

            <Dialog visible={deleteProductDialog} style={{ width: '32rem' }} breakpoints={{ '960px': '75vw', '641px': '90vw' }} header="Confirm" modal footer={deleteProductDialogFooter} onHide={hideDeleteProductDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                    {product && (
                        <span>
                            Are you sure you want to delete <b>{product.name}</b>?
                        </span>
                    )}
                </div>
            </Dialog>

            <Dialog visible={deleteProductsDialog} style={{ width: '32rem' }} breakpoints={{ '960px': '75vw', '641px': '90vw' }} header="Confirm" modal footer={deleteProductsDialogFooter} onHide={hideDeleteProductsDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem' }} />
                    {product && <span>Are you sure you want to delete the selected products?</span>}
                </div>
            </Dialog>
        </div>
    );
}