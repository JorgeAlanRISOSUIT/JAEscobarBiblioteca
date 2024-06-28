class Libro {

    constructor(isn, titulo, numeroPaginas, fecha, editorial, autor) {
        this.isn = isn
        this.titulo = titulo
        this.numeroPaginas = numeroPaginas
        this.fecha = fecha
        this.autor = autor
        this.editorial = editorial
    }

    get getISN() {
        return this.isn
    }

    get getTitulo() {
        return this.titulo
    }

    get getNumeroPaginas() {
        return this.numeroPaginas
    }

    get getFecha() {
        return this.fecha
    }

    set setISN(isn) {
        this.isn = isn
    }

    set setTitulo(titulo) {
        this.titulo = titulo
    }

    set setNumeroPaginas(numeroPaginas) {
        this.numeroPaginas = numeroPaginas
    }

    set setFecha(fecha) {
        this.fecha = fecha
    }



}

class Autor {
    constructor(idAutor, nombre) {
        this.idAutor = idAutor
        this.nombre = nombre
    }

    get getIdAutor() {
        return this.idAutor
    }

    get getNombre() {
        return this.nombre
    }

    set setIdAutor(idAutor) {
        this.idAutor = idAutor
    }

    set setNombre(nombre) {
        this.nombre = nombre
    }
}

class Editorial {
    constructor(idEditorial, nombre) {
        this.idEditorial = idEditorial
        this.nombre = nombre
    }

    get getIdEditorial() {
        return this.idEditorial
    }

    get getNombre() {
        return this.nombre
    }

    set setIdEditorial(idEditorial) {
        this.idEditorial = idEditorial
    }

    set setNombre(nombre) {
        this.nombre = nombre
    }
}