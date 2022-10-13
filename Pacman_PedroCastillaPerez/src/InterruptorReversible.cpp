#include "InterruptorReversible.hpp"
#include "NoEntiendo.hpp"

InterruptorReversible::InterruptorReversible(Pacman *_pacman, Tablero *_tablero, int _posX, int _posY, int _posControladaX, int _posControladaY):Interruptor(_pacman,_tablero,_posX,_posY,_posControladaX,_posControladaY)
{

}

InterruptorReversible::~InterruptorReversible()
{

}

void InterruptorReversible::Actualizar()
{
	if(pacman->ObtenerPosicionX() == posicionX && pacman->ObtenerPosicionY() == posicionY && !estaOn)
	{
		estaOn = true;
		tablero->PonerPared(posicionControladaX, posicionControladaY);
	}
	else if(pacman->ObtenerPosicionX() == posicionX && pacman->ObtenerPosicionY() == posicionY && estaOn)
	{
		estaOn = false;
		tablero->QuitarPared(posicionControladaX, posicionControladaY);
	}
}

/*void InterruptorReversible::Dibujar()
{
	NoEntiendo::PonerTipoTile(tilemap, posicionX, posicionY, INTERRUPTOR_OFF);
}*/
