#ifndef _INTERRUPTORREVERSIBLE_HPP_
#define _INTERRUPTORREVERSIBLE_HPP_

#include "Interruptor.hpp"


class InterruptorReversible: public Interruptor
{
	public:
		InterruptorReversible(Pacman *_pacman, Tablero *_tablero, int _posX, int _posY, int _posControladaX, int _posControladaY);
		virtual ~InterruptorReversible();
		void Actualizar();
		void Dibujar();
	
		const int tilemap = 3;
	
};

#endif