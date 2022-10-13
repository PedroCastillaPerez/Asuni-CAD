#ifndef _INTERRUPTORCONTIEMPO_HPP_
#define _INTERRUPTORCONTIEMPO_HPP_

#include "Interruptor.hpp"


class InterruptorConTiempo: public Interruptor
{
	public:
		InterruptorConTiempo(Pacman *_pacman, Tablero *_tablero, int _posX, int _posY, int _posControladaX, int _posControladaY);
		virtual ~InterruptorConTiempo();
		void Actualizar();
		void Dibujar();
	private:

		int temporizador;
		

		const int tiempoEspera = 25;
		const int tilemap = 3;
	
};

#endif