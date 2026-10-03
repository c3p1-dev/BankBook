CXX=        c++

CXXFLAGS=   -O2 -Wall -std=c++20 -Iinclude -I/usr/local/include -MMD -MP
LDFLAGS=    -L/usr/local/lib
LIBS=       -lsqlite3

OUT=        bankbook
SRCDIR=     src
OBJDIR=     build

SRCS!=      find ${SRCDIR} -type f -name '*.cpp'
OBJS=       ${SRCS:S#${SRCDIR}/#${OBJDIR}/:S/.cpp$/.o/}

all: ${OUT}

${OUT}: ${OBJS}
	${CXX} ${CXXFLAGS} ${LDFLAGS} -o ${.TARGET} ${OBJS} ${LIBS}

.for f in ${SRCS}
${OBJDIR}/${f:S#${SRCDIR}/##:S/.cpp$/.o/}: ${f}
	@mkdir -p ${.TARGET:H}
	${CXX} ${CXXFLAGS} -c ${.ALLSRC} -o ${.TARGET}
.endfor

.for o in ${OBJS}
.sinclude "${o:R}.d"
.endfor

clean:
	rm -rf ${OBJDIR} ${OUT}

run: ${OUT}
	./${OUT}

.PHONY: all clean run