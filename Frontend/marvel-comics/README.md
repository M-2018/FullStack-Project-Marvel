# Marvel Comics - FullStack Project

Este es un proyecto de aplicación web en Angular que permite la visualización de cómics y sus detalles, utilizando servicios para la autenticación de usuarios. Los servicios implementan patrones de diseño como el patrón Observer para la gestión de estados y la comunicación de componentes.

## Requerimientos

Antes de comenzar, asegúrate de tener instalados los siguientes programas en tu máquina:

- [Node.js](https://nodejs.org/) (Recomendado: versión LTS)
- [Angular CLI](https://angular.io/cli) (Recomendado: última versión estable)

## Clonar el Repositorio

Clona el repositorio utilizando el siguiente comando:

```bash
git clone https://github.com/M-2018/FullStack-Project-Marvel.git

Instalación
Una vez que hayas clonado el repositorio, navega a la carpeta del proyecto y ejecuta el siguiente comando para instalar las dependencias:
npm install

Paquetes Instalados
El proyecto utiliza los siguientes paquetes principales:

@angular/animations: Para animaciones en Angular.
@angular/cdk y @angular/material: Para la implementación de componentes de UI de Material Design.
@angular/forms: Para formularios reactivos y de plantilla en Angular.
bootstrap: Para el diseño responsivo y componentes de UI básicos.
jwt-decode: Para la decodificación de tokens JWT.
rxjs: Para la gestión de operaciones asíncronas y eventos reactivos.
Servicios y Patrones de Diseño
Autenticación
El proyecto implementa un servicio de autenticación que utiliza el patrón Observer. Este patrón permite que los componentes de la aplicación reaccionen a cambios en el estado de autenticación, como el inicio o cierre de sesión de un usuario. Los componentes que requieren acceso a información protegida están suscritos a estos cambios de estado, lo que asegura que se actualicen en tiempo real sin necesidad de recargar la página.

Otros Patrones
El código sigue prácticas modernas y patrones de diseño recomendados en Angular, como la separación de responsabilidades, la inyección de dependencias y el uso de servicios para la comunicación entre componentes.

Ejecución
Una vez que hayas instalado las dependencias, ejecuta el siguiente comando para iniciar el servidor de desarrollo:
npm start

Comandos
ng serve --o: Inicia el servidor de desarrollo y abre la aplicación automáticamente en el navegador.
ng build: Construye la aplicación para producción.

Licencia
Este proyecto está licenciado bajo la Licencia MIT.