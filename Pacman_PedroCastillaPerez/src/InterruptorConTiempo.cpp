#include "InterruptorConTiempo.hpp"
#include "NoEntiendo.hpp"

InterruptorConTiempo::InterruptorConTiempo(Pacman *_pacman, Tablero *_tablero, int _posX, int _posY, int _posControladaX, int _posControladaY):Interruptor(_pacman,_tablero,_posX,_posY,_posControladaX,_posControladaY)
{
	temporizador = 0;
}

InterruptorConTiempo::~InterruptorConTiempo()
{

}

void InterruptorConTiempo::Actualizar()
{
	if(temporizador > tiempoEspera)
	{
		estaOn = true;
		tablero->PonerPared(posicionControladaX, posicionControladaY);
		temporizador = 0;
	}
	else if(pacman->ObtenerPosicionX() == posicionX && pacman->ObtenerPosicionY() == posicionY && estaOn)
	{
		estaOn = false;
		tablero->QuitarPared(posicionControladaX, posicionControladaY);
		temporizador ++;
	}

	if(temporizador > 0)
	{
		temporizador ++;
	}
}

/*void InterruptorReversible::Dibujar()
{
	NoEntiendo::PonerTipoTile(tilemap, posicionX, posicionY, INTERRUPTOR_OFF);
}*/
