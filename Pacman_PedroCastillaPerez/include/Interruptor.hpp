#ifndef _INTERRUPTOR_HPP_
#define _INTERRUPTOR_HPP_

#include "Tablero.hpp"
#include "Pacman.hpp"

enum InterruptorTile
{
	INTERRUPTOR_ON = 19,
	INTERRUPTOR_OFF

};


class Interruptor
{
	public:
		Interruptor(Pacman *_pacman, Tablero *_tablero, int _posX, int _posY, int _posControladaX, int _posControladaY);
		virtual ~Interruptor();
		virtual void Actualizar();
		void Dibujar();

	protected:
		bool estaOn;
		int posicionX;
		int posicionY;

		int posicionControladaX;
		int posicionControladaY;
		
		Pacman *pacman;
		Tablero *tablero;
		
		const int tilemap = 3;
	
};

#endif