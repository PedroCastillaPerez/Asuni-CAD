#include "Interruptor.hpp"
#include "NoEntiendo.hpp"

Interruptor::Interruptor(Pacman *_pacman, Tablero *_tablero, int _posX, int _posY, int _posControladaX, int _posControladaY)
{
	/*_posX = 7;
	_posY = 10;
	_posControladaX = 0;
	_posControladaY = 0;*/

	estaOn = true;
	posicionX = _posX;
	posicionY = _posY;

	posicionControladaX = _posControladaX;
	posicionControladaY = _posControladaY;

	pacman = _pacman;
	tablero = _tablero;

}

Interruptor::~Interruptor()
{
}

void Interruptor::Actualizar()
{

}

void Interruptor::Dibujar()
{
	if(estaOn)
	{
		NoEntiendo::PonerTipoTile(tilemap, posicionX, posicionY, INTERRUPTOR_ON);
	}
	else
	{
		NoEntiendo::PonerTipoTile(tilemap, posicionX, posicionY, INTERRUPTOR_OFF);
	}
}

