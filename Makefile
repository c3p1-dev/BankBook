CXX=c++
CXXFLAGS=-O2 -Wall -std=c++20 -Iinclude

OUT=bankbook

SRCS=src/main.cpp
OBJS=obj/main.o

all: ${OUT}

obj:
	mkdir -p obj

obj/main.o: src/main.cpp | obj
	${CXX} ${CXXFLAGS} -c $< -o $@

${OUT}: ${OBJS}
	${CXX} ${CXXFLAGS} -o $@ ${OBJS}

clean:
	rm -rf obj ${OUT}
